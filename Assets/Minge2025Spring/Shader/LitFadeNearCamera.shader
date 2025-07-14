// シェーダーのパスと名前を定義
Shader "My Shaders/Unlit Fade Near Camera Textured"
{
    // Inspectorに表示されるプロパティを定義
    Properties
    {
        [MainTexture] _BaseMap("Base Map (Texture)", 2D) = "white" {} // --- 追加: テクスチャ ---
        [MainColor] _Color("Color Tint", Color) = (1, 1, 1, 1)        // --- 変更: テクスチャの色を調整するTintに ---
        _FadeStartDistance("Fade Start Distance", Range(0.0, 50.0)) = 5.0
        _FadeEndDistance("Fade End Distance", Range(0.0, 50.0)) = 1.0
    }

    // シェーダーの本体
    SubShader
    {
        // 透明オブジェクトとして扱われるようにタグを設定
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        // 描画パス
        Pass
        {
            // 透明ブレンドの設定
            Blend SrcAlpha OneMinusSrcAlpha
            // 裏面をカリング（描画しない）
            Cull Back
            // 深度バッファへの書き込みをオフにする
            ZWrite Off

            // HLSLプログラムの開始
            HLSLPROGRAM

            // 頂点シェーダーとフラグメントシェーダーの関数名を指定
            #pragma vertex vert
            #pragma fragment frag

            // URPの基本的なライブラリをインクルード
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // CBUFFER: プロパティをシェーダー内で使えるようにする
            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
                float _FadeStartDistance;
                float _FadeEndDistance;
                // --- 追加: テクスチャのタイリングとオフセット情報 ---
                float4 _BaseMap_ST; 
            CBUFFER_END
            
            // --- 追加: テクスチャとサンプラーを宣言 ---
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            // 頂点シェーダーへの入力データ構造
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0; // --- 追加: UV座標 ---
            };

            // 頂点シェーダーからフラグメントシェーダーへ渡すデータ構造
            struct Varyings
            {
                float4 positionCS     : SV_POSITION;
                float3 positionWS     : TEXCOORD0; // ワールド座標
                float2 uv             : TEXCOORD1; // --- 追加: UV座標 ---
            };

            // 頂点シェーダー
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionCS = TransformWorldToHClip(OUT.positionWS);
                // --- 追加: UV座標を計算してVaryingsに渡す ---
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            // フラグメントシェーダー
            half4 frag(Varyings IN) : SV_Target
            {
                // --- 変更: テクスチャから色を取得し、Colorで色付けする ---
                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _Color;

                // カメラからピクセルまでの距離を計算
                float dist = distance(IN.positionWS, _WorldSpaceCameraPos);

                // 距離に基づいてアルファ値を計算
                float fadeAlpha = smoothstep(_FadeEndDistance, _FadeStartDistance, dist);
                
                // テクスチャのアルファ値と、距離によるアルファ値を乗算する
                baseColor.a *= fadeAlpha;

                return baseColor;
            }

            ENDHLSL
        }
    }
}