using System;
using System.Text;
using OpenTK;
using Pastel.Core.Models;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Templates.MacOS
{
    public class Square: PastelObject
    {
        bool forward = true;
        int red, green, blue;
        Vector2 P1, P2, P3, P4;
        private VertexPositionColor[] quadVertices;
        private ushort[] quadIndices;
        private DeviceBuffer vertexBuffer;
        private DeviceBuffer indexBuffer;

        public Square(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }


        public void CreateResources()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            vertexBuffer = factory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
            indexBuffer = factory.CreateBuffer(new BufferDescription(4 * sizeof(ushort), BufferUsage.IndexBuffer));

            

        }

        public override void Update()
        {

            if (forward)
            {
                if (red == 64)
                {
                    if (green == 64)
                    {
                        if (blue == 64)
                        {
                            forward = false;
                        }
                        else
                        {
                            blue += 1;
                        }
                    }
                    else
                    {
                        green += 1;
                    }
                }
                else
                {
                    red += 1;
                }
            }
            else
            {
                if (red == 0)
                {
                    if (green == 0)
                    {
                        if (blue == 0)
                        {
                            forward = true;
                        }
                        else
                        {
                            blue -= 1;
                        }
                    }
                    else
                    {
                        green -= 1;
                    }
                }
                else
                {
                    red -= 1;
                }
            }
        }

        public override void Draw()
        {
            //base.Draw();
            quadVertices = new[]
            {
                new VertexPositionColor(
                    P1,
                    new RgbaFloat(red / 64f, green / 64f, blue / 64f, 1f)),
                new VertexPositionColor(
                    P2,
                    new RgbaFloat(red / 64f, blue / 64f, green / 64f, 1f)),
                new VertexPositionColor(
                    P3,
                    new RgbaFloat(green / 64f, blue / 64f, red / 64f, 1f)),
                new VertexPositionColor(
                    P4,
                    new RgbaFloat(blue / 64f, red / 64f, green / 64f, 1f))
            };

            quadIndices = new ushort[] { 0, 1, 2, 3 };

            graphicsDevice.UpdateBuffer(vertexBuffer, 0, quadVertices);
            graphicsDevice.UpdateBuffer(indexBuffer, 0, quadIndices);

            PastelGame.CommandList.SetVertexBuffer(0, vertexBuffer);
            PastelGame.CommandList.SetIndexBuffer(indexBuffer, IndexFormat.UInt16);
            PastelGame.CommandList.SetPipeline(Pipeline);
            PastelGame.CommandList.DrawIndexed(
                indexCount: 4,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);
        }

        public override void Dispose()
        {
            Pipeline.Dispose();
            vertexBuffer.Dispose();
            indexBuffer.Dispose();
        }
    }

    struct VertexPositionColor
    {
        public Vector2 Position; // This is the position, in normalized device coordinates.
        public RgbaFloat Color; // This is the color of the vertex.
        public VertexPositionColor(Vector2 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }
        public const uint SizeInBytes = 24;
    }
}
