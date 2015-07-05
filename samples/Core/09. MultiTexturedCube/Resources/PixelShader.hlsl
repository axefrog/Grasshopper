Texture2DArray<float4> ShaderTexture : register(t0);

SamplerState Sampler : register(s0);

struct VOut
{
    float4 position : SV_POSITION;
    float2 texcoord: TEXCOORD;
    uint face : CUSTOM;
};

float4 PSMain(VOut input) : SV_Target
{
    int textureIndex = 0;
    [flatten]
    switch (input.face)
    {
        case 0: textureIndex = 1; break; // front
        case 1: textureIndex = 1; break; // back
        case 2: textureIndex = 1; break; // left
        case 3: textureIndex = 1; break; // right
        case 4: textureIndex = 0; break; // top
        case 5: textureIndex = 2; break; // bottom
    }
    return ShaderTexture.Sample(Sampler, float3(input.texcoord, textureIndex));
}