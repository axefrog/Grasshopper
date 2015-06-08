Texture2DArray<float4> ShaderTexture : register(t0);

SamplerState Sampler : register(s0);

struct VOut
{
	float4 position : SV_POSITION;
	float2 texcoord: TEXCOORD;
};

float4 PSMain(VOut input) : SV_Target
{
	return ShaderTexture.Sample(Sampler, float3(input.texcoord, 2));
}