cbuffer ViewData : register(b0)
{
    float4x4 view;
    float4x4 projection;
    float secondsElapsed;
}

struct VOut
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
};

struct VIn
{
    float4 position: POSITION;
    float4 color: COLOR;

    // instance data
    float4 cubePosition: CUSTOM0;
    float4 cubeRotation: CUSTOM1;
    float4 cubeScale: CUSTOM2;
};

static float PI = 3.1415926535897932384626433832795;
float4x4 calculateRotationMatrix(float4 rotation)
{
    float3 axis = normalize(float3(rotation.x, rotation.y, rotation.z));
    float angle = secondsElapsed * rotation.w * PI * 2;
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    return float4x4(oc * axis.x * axis.x + c, oc * axis.x * axis.y - axis.z * s, oc * axis.z * axis.x + axis.y * s, 0.0,
                    oc * axis.x * axis.y + axis.z * s, oc * axis.y * axis.y + c, oc * axis.y * axis.z - axis.x * s, 0.0,
                    oc * axis.z * axis.x - axis.y * s, oc * axis.y * axis.z + axis.x * s, oc * axis.z * axis.z + c, 0.0,
                    0.0, 0.0, 0.0, 1.0);
}

VOut VSMain(VIn input)
{
    float4 vertexPosition = float4(input.position.x * input.cubeScale.x, input.position.y * input.cubeScale.y, input.position.z * input.cubeScale.z, 1);
    float4x4 rotation = calculateRotationMatrix(input.cubeRotation);

    VOut output;
    output.position = mul(vertexPosition, rotation) + input.cubePosition;
    output.position = mul(mul(output.position, view), projection);
    output.color = input.color;
    return output;
}
