Shader "TransparencyDithering"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		CGPROGRAM
		#pragma surface surf Standard

		half4 _Color;
		sampler3D	_DitherMaskLOD;

		struct Input
		{
			float4 screenPos;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			half alphaRef = tex3D(_DitherMaskLOD, float3(IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy * 0.25, _Color.a*0.9375)).a;
			clip(alphaRef - 0.01);

			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}

	FallBack "Differd"
}