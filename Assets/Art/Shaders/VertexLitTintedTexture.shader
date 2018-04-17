Shader "Mobile/Vertex Lit Tinted Texture" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}
 
SubShader {
	Tags {"Queue"="Geometry"  "IgnoreProjector"="True"}
	LOD 100
 
	ZWrite On
 
	Pass {
		Tags { LightMode = Vertex } 
		CGPROGRAM
		#pragma vertex vert  
		#pragma fragment frag
 
		#include "UnityCG.cginc"
 
		fixed4 _Color;
 
		sampler2D _MainTex;
		float4 _MainTex_ST;
 
		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv_MainTex : TEXCOORD0;
			fixed3 diff : COLOR;
		};
 
		v2f vert (appdata_full v)
		{
		    v2f o;
		    o.pos = UnityObjectToClipPos (v.vertex);
		    o.uv_MainTex = TRANSFORM_TEX (v.texcoord, _MainTex);
 
			float3 viewpos = UnityObjectToViewPos (v.vertex).xyz;
 
			o.diff = UNITY_LIGHTMODEL_AMBIENT.xyz;
 
			//All calculations are in object space
			for (int i = 0; i < 4; i++) {
				half3 toLight = unity_LightPosition[i].xyz - viewpos.xyz * unity_LightPosition[i].w;
				half lengthSq = dot(toLight, toLight);
				half atten = 1.0 / (1.0 + lengthSq * unity_LightAtten[i].z );
 
				fixed3 lightDirObj = mul( (float3x3)UNITY_MATRIX_T_MV, toLight);	//View => model
 
				lightDirObj = normalize(lightDirObj);
 
				fixed diff = max ( 0, dot (v.normal, lightDirObj) );
				o.diff += unity_LightColor[i].rgb * (diff * atten);
			}
 
			o.diff = o.diff * _Color;
 
			return o;
		}
 
		fixed4 frag (v2f i) : COLOR {
			fixed4 c;
 
			fixed4 mainTex = tex2D (_MainTex, i.uv_MainTex);
 
			c.rgb = (mainTex.rgb * i.diff);
			c.a = 1;
 
			return c;
		}
 
		ENDCG
	}
}
 
Fallback "VertexLit"
}