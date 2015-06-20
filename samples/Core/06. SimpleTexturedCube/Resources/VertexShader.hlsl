cbuffer ViewData : register(b0)
{
	float4x4 worldViewProjection;
}

struct VOut
{
	float4 position : SV_POSITION;
	float2 texcoord: TEXCOORD;
};

struct VIn
{
	float4 position: POSITION;
	float4 color: COLOR;
	float2 texcoord: TEXCOORD;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = mul(input.position, worldViewProjection);
	output.texcoord = input.texcoord;
	return output;
}
