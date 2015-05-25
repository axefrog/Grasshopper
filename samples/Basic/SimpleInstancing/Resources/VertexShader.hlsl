struct VOut
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

struct VIn
{
	// standard vertex elements
	float4 position: POSITION;
	float4 color: COLOR;
	float2 texcoord: TEXCOORD;
	float2 pad: PADDING0;

	// instance elements
	float4x4 translation: CUSTOM0;
};

VOut VSMain(VIn input)
{
	VOut output;
	output.position = mul(input.translation, input.position);
	output.color = input.color;
	return output;
}
