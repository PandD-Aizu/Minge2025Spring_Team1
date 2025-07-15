Shader "My Shaders/OccludedOutline_Final"
{
 Properties
 {
  [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
  _Color ("Tint", Color) = (1,1,1,1)
  [Header(Outline Settings)]
  _OutlineThickness ("Outline Thickness", Range(0, 5)) = 1
  _OutlineColor ("Outline Color", Color) = (1, 1, 0, 1)
 }

 SubShader
 {
  Tags
  {
   "Queue"="Transparent"
   "RenderType"="Transparent"
   "CanUseSpriteAtlas"="True"
  }

  // ===================================================================
  // パス 0: 隠れた時のアウトラインを描画し、ステンシルに「1」を書き込む
  // ===================================================================
  Pass
  {
   Cull Off
   Lighting Off
   ZWrite Off
   Blend SrcAlpha OneMinusSrcAlpha
   ZTest Greater // ★隠れている時のみ描画

   // ステンシル設定：このパスが描画したピクセルに目印(1)を付ける
   Stencil
   {
    Ref 1
    Comp Always
    Pass Replace
   }

   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #include "UnityCG.cginc"

   struct appdata { float4 vertex : POSITION; float2 texcoord : TEXCOORD0; };
   struct v2f { float4 vertex : SV_POSITION; float2 texcoord : TEXCOORD0; };

   sampler2D _MainTex;
   float4 _MainTex_TexelSize;
   float _OutlineThickness;
   fixed4 _OutlineColor;

   v2f vert(appdata IN) 
   { 
       v2f OUT; 
       OUT.vertex = UnityObjectToClipPos(IN.vertex); 
       OUT.texcoord = IN.texcoord; 
       return OUT; 
   }

   fixed4 frag(v2f IN) : SV_Target
   {
    fixed center_alpha = tex2D(_MainTex, IN.texcoord).a;
    float2 offset = _MainTex_TexelSize.xy * _OutlineThickness;
    fixed max_neighbor_alpha = 0;
    max_neighbor_alpha = max(max_neighbor_alpha, tex2D(_MainTex, IN.texcoord + float2( offset.x,  0)).a);
    max_neighbor_alpha = max(max_neighbor_alpha, tex2D(_MainTex, IN.texcoord + float2(-offset.x,  0)).a);
    max_neighbor_alpha = max(max_neighbor_alpha, tex2D(_MainTex, IN.texcoord + float2( 0,  offset.y)).a);
    max_neighbor_alpha = max(max_neighbor_alpha, tex2D(_MainTex, IN.texcoord + float2( 0, -offset.y)).a);

    fixed outline_value = saturate(max_neighbor_alpha - center_alpha);
    fixed4 final_color = _OutlineColor;
    final_color.a *= outline_value;
    clip(final_color.a - 0.01);
    return final_color;
   }
   ENDCG
  }

  // ===================================================================
  // パス 1: 見えている時のスプライト本体を描画。ただしステンシルの目印がない部分のみ
  // ===================================================================
  Pass
  {
   Cull Off
   Lighting Off
   ZWrite Off
   Blend SrcAlpha OneMinusSrcAlpha
   ZTest LEqual // ★見えている時のみ描画

   // ステンシル設定：目印(1)が付いていないピクセルのみ描画する
   Stencil
   {
    Ref 1
    Comp NotEqual // ステンシル値が1でなければ描画
   }

   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #pragma multi_compile _ PIXELSNAP_ON
   #include "UnityCG.cginc"

   struct appdata { float4 vertex : POSITION; float4 color : COLOR; float2 texcoord : TEXCOORD0; };
   struct v2f { float4 vertex : SV_POSITION; fixed4 color : COLOR; float2 texcoord : TEXCOORD0; };

   sampler2D _MainTex;
   fixed4 _Color;

   v2f vert(appdata IN) 
   { 
       v2f OUT; 
       OUT.vertex = UnityObjectToClipPos(IN.vertex); 
       OUT.texcoord = IN.texcoord; 
       OUT.color = IN.color * _Color; 
       #ifdef PIXELSNAP_ON 
       OUT.vertex = UnityPixelSnap(OUT.vertex); 
       #endif 
       return OUT; 
   }

   fixed4 frag(v2f IN) : SV_Target
   {
    fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
    clip(color.a - 0.01);
    return color;
   }
   ENDCG
  }
 }
}