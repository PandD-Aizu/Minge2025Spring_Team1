Shader "My Shaders/UnLitPixelization"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", float) = 100.0
        
        _SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
        _SpecMap("Specular Map (R)", 2D) = "white" {}
        _Shininess("Shininess", Range(0.01, 1)) = 0.5
        _BumpMap("Normal Map", 2D) = "bump" {}
    }
    
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "LightMode"="UniversalForward" }
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            # include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            # include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            /// 変数宣言
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _PixelSize;
                half4 _SpecColor;
                half _Shininess;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_SpecMap);
            SAMPLER(sampler_SpecMap);
            TEXTURE2D(_BumpMap);
            SAMPLER(sampler_BumpMap);

            /// 構造体
            // 頂点属性とバリエーションを定義
            struct Attributes
            {
                float4 positionOS   : POSITION; // OS = Object Space
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
                float2 uv           : TEXCOORD0;
            };

            // バリエーション構造体
            struct Varyings
            {
                float4 positionCS   : SV_POSITION; // CS = Clip Space
                float2 uv           : TEXCOORD0;
                float3 positionWS   : TEXCOORD1; // WS = World Space
                float3 normalWS     : TEXCOORD2;
                float3 tangentWS    : TEXCOORD3;
                float3 bitangentWS  : TEXCOORD4;
            };

            /// <summary>
            /// 頂点シェーダー
            /// <summary>
            /// <param name="IN">入力属性</param>
            /// <returns>出力バリエーション</returns>
            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                // URPのヘルパー関数を使用して座標変換
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionCS = TransformWorldToHClip(OUT.positionWS);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                
                // タンジェントと従法線の計算
                OUT.tangentWS = TransformObjectToWorldDir(IN.tangentOS.xyz);
                OUT.bitangentWS = cross(OUT.normalWS, OUT.tangentWS) * IN.tangentOS.w;
                
                OUT.uv = IN.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                return OUT;
            }

            /// <summary>
            /// フラグメントシェーダー
            /// <summary>
            /// <param name="IN">入力バリエーション</param>
            /// <retuns>出力カラー</returns>
            half4 frag (Varyings IN) : SV_Target
            {
                float2 snappedUV = (floor(IN.uv * _PixelSize) + 0.5) / _PixelSize;

                // テクスチャサンプリング
                half4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, snappedUV);
                half specMask = SAMPLE_TEXTURE2D(_SpecMap, sampler_SpecMap, snappedUV).r;
                half3 packedNormal = SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, snappedUV).rgb;

                // 法線マッピング
                float3x3 TBN = float3x3(normalize(IN.tangentWS), normalize(IN.bitangentWS), normalize(IN.normalWS));
                float3 worldNormal = UnpackNormal(half4(packedNormal, 1.0));
                worldNormal = mul(worldNormal, TBN);
                worldNormal = normalize(worldNormal);

                // 各種ベクトルの準備
                Light mainLight = GetMainLight();
                half3 lightColor = mainLight.color;
                half3 lightDir = mainLight.direction; // ライト"へ"の方向ベクトル
                half3 viewDir = normalize(_WorldSpaceCameraPos.xyz - IN.positionWS);
                half3 halfDir = normalize(lightDir + viewDir);

                // Ambient
                half3 ambient = half3(0.05, 0.05, 0.05) * albedo.rgb;

                // Diffuse
                half NdotL = saturate(dot(worldNormal, lightDir));
                half3 diffuse = lightColor * albedo.rgb * NdotL;

                // Specular
                half NdotH = saturate(dot(worldNormal, halfDir));
                half specPower = pow(NdotH, _Shininess * 128);
                half3 specular = lightColor * _SpecColor.rgb * specPower * specMask;

                // 最終的な色を合成して返す
                half3 finalColor = ambient + diffuse + specular;

                return half4(finalColor, albedo.a);
            }

            ENDHLSL
        }
    }
}
