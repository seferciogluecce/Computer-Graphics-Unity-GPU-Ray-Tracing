using System.Collections.Generic;
using UnityEngine;
public class RayTracingMaster2 : MonoBehaviour
{
    public ComputeShader RayTracingShader;
   public Texture SkyboxTexture;
    public Light DirectionalLight;

    public Vector2 SphereRadius = new Vector2(3.0f, 8.0f);
    public uint SpheresMax = 5;
    public uint SquaresMax = 0;
    
    public float SpherePlacementRadius = 20.0f;

    private Camera _camera;
    private float _lastFieldOfView;
    private Material _addMaterial;
    private RenderTexture _target;
    private uint _currentSample = 0;
    private ComputeBuffer _sphereBuffer;

    struct Sphere
    {
      public  Vector3 position;
        public float radius;
        public Vector3 albedo;
        public Vector3 specular;
    };

 


    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }


    private void OnEnable()
    {
        _currentSample = 0;
        SetUpScene();
    }
    private void OnDisable()
    {
        if (_sphereBuffer != null)
            _sphereBuffer.Release();
    }

    private void Update()
    {

        if (_camera.fieldOfView != _lastFieldOfView)
        {
            _currentSample = 0;
            _lastFieldOfView = _camera.fieldOfView;
        }
        if (transform.hasChanged)
        {
            _currentSample = 0;
            transform.hasChanged = false;
        }
        if (DirectionalLight.transform.hasChanged)
        {
            _currentSample = 0;
            DirectionalLight.transform.hasChanged = false;
        }
    }

    private void SetUpScene()
    {
        List<Sphere> spheres = new List<Sphere>();

        // Add a number of random spheres
        for (int i = 0; i < SpheresMax; i++)
        {
            Sphere sphere = new Sphere();

            // Radius and radius
            sphere.radius = 50;
                //SphereRadius.x + Random.value * (SphereRadius.y - SphereRadius.x);
            Vector2 randomPos = Random.insideUnitCircle * SpherePlacementRadius;
            sphere.position = Vector3.zero;
                //new Vector3(randomPos.x, sphere.radius, randomPos.y);

            // Reject spheres that are intersecting others
            foreach (Sphere other in spheres)
            {
                float minDist = sphere.radius + other.radius;
                if (Vector3.SqrMagnitude(sphere.position - other.position) < minDist * minDist)
                    goto SkipSphere;
            }

            // Albedo and specular color
            Color color = Random.ColorHSV();
            bool metal = Random.value < 0.5f;
            metal = true;
            sphere.albedo = metal ? Vector4.zero : new Vector4(color.r, color.g, color.b);
            sphere.specular = metal ? new Vector4(color.r, color.g, color.b) : new Vector4(0.04f, 0.04f, 0.04f);

            // Add the sphere to the list
            spheres.Add(sphere);

        SkipSphere:
            continue;
        }

        // Assign to compute buffer
        if (_sphereBuffer != null)
            _sphereBuffer.Release();
        if (spheres.Count > 0)
        {
            _sphereBuffer = new ComputeBuffer(spheres.Count, 40);
            _sphereBuffer.SetData(spheres);
        }
    }


    private void SetShaderParameters()
    {
        RayTracingShader.SetTexture(0, "_SkyboxTexture", SkyboxTexture);
        RayTracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        RayTracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        RayTracingShader.SetVector("_PixelOffset", new Vector2(Random.value, Random.value));

        Vector3 l = DirectionalLight.transform.forward;
        RayTracingShader.SetVector("_DirectionalLight", new Vector4(l.x, l.y, l.z, DirectionalLight.intensity));

        if (_sphereBuffer != null)
            RayTracingShader.SetBuffer(0, "_Spheres", _sphereBuffer);

    }

    private void InitRenderTexture()
    {
        if (_target == null || _target.width != Screen.width || _target.height != Screen.height)
        {
            // Release render texture if we already have one
            if (_target != null)
                _target.Release();
            // Get a render target for Ray Tracing
            _target = new RenderTexture(Screen.width, Screen.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            _target.enableRandomWrite = true;
            _target.Create();


            // Reset sampling
            _currentSample = 0;
        }
    }

    private void Render(RenderTexture destination)
    {
        // Make sure we have a current render target
        InitRenderTexture();
        // Set the target and dispatch the compute shader
       RayTracingShader.SetTexture(0, "Result", _target);
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        RayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);
        // Blit the result texture to the screen
        Graphics.Blit(_target, destination);

       // Blit the result texture to the screen
        if (_addMaterial == null)
            _addMaterial = new Material(Shader.Find("Hidden/AddShader2"));
        _addMaterial.SetFloat("_Sample", _currentSample);
        Graphics.Blit(_target, destination, _addMaterial);
        _currentSample++;

    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        SetShaderParameters();
        Render(destination);
    }
}