struct VOut
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

struct VIn
{
	float4 position: POSITION0;
	float4 color: COLOR0;
	float2 texcoord: TEXCOORD0;
	float2 pad: PADDING0;
	float4x4 translation: CUSTOM0;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = mul(input.translation, input.position);
	output.color = input.color;
	return output;
}
