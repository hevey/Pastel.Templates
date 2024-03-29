﻿using System;
using System.Numerics;
using System.Text;
using Pastel.Core.Models;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Test
{
    public class ComputedTexture : PastelObject
    {
        protected const string VertexCode = @"
#version 450

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

layout(location = 0) out vec4 fsin_Color;

void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";

        protected const string FragmentCode = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";

        private Shader[]? _shaders;
        protected readonly GraphicsDevice GraphicsDevice;

        protected Pipeline? Pipeline;
        protected Vector2 Position;

        protected ComputedTexture()
        {
            GraphicsDevice = PastelGame.GraphicsDevice;
            Initialize();
        }

        private void Initialize()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            var vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate,
                    VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate,
                    VertexElementFormat.Float4));

            var vertexShaderDesc = new ShaderDescription(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(VertexCode),
                "main");
            var fragmentShaderDesc = new ShaderDescription(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(FragmentCode),
                "main");
            _shaders = factory.CreateFromSpirv(vertexShaderDesc, fragmentShaderDesc);

            var pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;

            pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
                true,
                true,
                ComparisonKind.LessEqual);

            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                FaceCullMode.Back,
                PolygonFillMode.Solid,
                FrontFace.Clockwise,
                true,
                false);

            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            pipelineDescription.ResourceLayouts = Array.Empty<ResourceLayout>();

            pipelineDescription.ShaderSet = new ShaderSetDescription(
                new[] { vertexLayout },
                _shaders);

            pipelineDescription.Outputs = PastelGame.GraphicsDevice.SwapchainFramebuffer.OutputDescription;
            Pipeline = factory.CreateGraphicsPipeline(pipelineDescription);
        }
    }
}
