using System;
using System.Text;
using System.Numerics;
using Pastel.Core.Models;
using Veldrid;
using Veldrid.SPIRV;

namespace Pastel.Templates.MacOS
{
    public class Rectangle: PastelObject
    {
        public float Size;
        bool forward = true;
        int red, green, blue;

        private Vector2 p1, p2, p3, p4;
        private VertexPositionColor[] quadVertices;
        private ushort[] quadIndices;
        private DeviceBuffer vertexBuffer;
        private DeviceBuffer indexBuffer;

        public Rectangle(Vector2 position, float size)
        {
            Position = position;
            Size = size;
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

            var screenSize = Size / 100f;
            p1 = new Vector2(Position.X - screenSize, Position.Y + screenSize);
            p2 = new Vector2(Position.X + screenSize, Position.Y + screenSize);
            p3 = new Vector2(Position.X - screenSize, Position.Y - screenSize);
            p4 = new Vector2(Position.X + screenSize, Position.Y - screenSize);

        }

        public override void Draw()
        {
            quadVertices = new[]
            {
                new VertexPositionColor(
                    p1,
                    new RgbaFloat(red / 64f, green / 64f, blue / 64f, 1f)),
                new VertexPositionColor(
                    p2,
                    new RgbaFloat(red / 64f, blue / 64f, green / 64f, 1f)),
                new VertexPositionColor(
                    p3,
                    new RgbaFloat(green / 64f, blue / 64f, red / 64f, 1f)),
                new VertexPositionColor(
                    p4,
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
