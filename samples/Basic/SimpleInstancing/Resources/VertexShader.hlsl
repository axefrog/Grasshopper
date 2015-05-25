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
	float4 pos = float4(input.position.x / 4, input.position.y / 4, 0, 1.0);
	pos = mul(input.translation, pos);
	output.position = pos;
	output.color = input.color;
	return output;
}
