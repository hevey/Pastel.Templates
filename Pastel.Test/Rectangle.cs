using System.Numerics;
using Pastel.Core.Models;
using Pastel.Core.Platform.Input;
using Veldrid;

namespace Pastel.Test
{
    public class Rectangle : PastelObject
    {
        public float Size;
        
        private bool _forward = true;
        private DeviceBuffer _indexBuffer;
        private DeviceBuffer _vertexBuffer;
        private Vector2 _p1, _p2, _p3, _p4;
        private ushort[] _quadIndices;
        private VertexPositionColor[] _quadVertices;
        private int _red, _green, _blue;
        

        public Rectangle(Vector2 position, float size)
        {
            Position = position;
            Size = size;
        }


        public void CreateResources()
        {
            var factory = PastelGame.GraphicsDevice.ResourceFactory;

            _vertexBuffer =
                factory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes,
                    BufferUsage.VertexBuffer));
            _indexBuffer = factory.CreateBuffer(new BufferDescription(4 * sizeof(ushort), BufferUsage.IndexBuffer));
        }

        public override void Update(float deltaTime)
        {
            if (_forward)
            {
                if (_red == 64)
                {
                    if (_green == 64)
                    {
                        if (_blue == 64)
                            _forward = false;
                        else
                            _blue += 1;
                    }
                    else
                    {
                        _green += 1;
                    }
                }
                else
                {
                    _red += 1;
                }
            }
            else
            {
                if (_red == 0)
                {
                    if (_green == 0)
                    {
                        if (_blue == 0)
                            _forward = true;
                        else
                            _blue -= 1;
                    }
                    else
                    {
                        _green -= 1;
                    }
                }
                else
                {
                    _red -= 1;
                }
            }

            if (InputManager.Buttons.Find(b => b.Name == "Up").Pressed) Position.Y += 0.005f * deltaTime;
            if (InputManager.Buttons.Find(b => b.Name == "Down").Pressed) Position.Y -= 0.005f * deltaTime;
            if (InputManager.Buttons.Find(b => b.Name == "Left").Pressed) Position.X -= 0.005f * deltaTime;
            if (InputManager.Buttons.Find(b => b.Name == "Right").Pressed) Position.X += 0.005f * deltaTime;


            var screenSize = Size / 100f;
            _p1 = new Vector2(Position.X - screenSize, Position.Y + screenSize);
            _p2 = new Vector2(Position.X + screenSize, Position.Y + screenSize);
            _p3 = new Vector2(Position.X - screenSize, Position.Y - screenSize);
            _p4 = new Vector2(Position.X + screenSize, Position.Y - screenSize);
        }

        public override void Draw()
        {
            _quadVertices = new[]
            {
                new VertexPositionColor(
                    _p1,
                    new RgbaFloat(_red / 64f, _green / 64f, _blue / 64f, 1f)),
                new VertexPositionColor(
                    _p2,
                    new RgbaFloat(_red / 64f, _blue / 64f, _green / 64f, 1f)),
                new VertexPositionColor(
                    _p3,
                    new RgbaFloat(_green / 64f, _blue / 64f, _red / 64f, 1f)),
                new VertexPositionColor(
                    _p4,
                    new RgbaFloat(_blue / 64f, _red / 64f, _green / 64f, 1f))
            };

            _quadIndices = new ushort[] {0, 1, 2, 3};

            GraphicsDevice.UpdateBuffer(_vertexBuffer, 0, _quadVertices);
            GraphicsDevice.UpdateBuffer(_indexBuffer, 0, _quadIndices);

            PastelGame.CommandList.SetVertexBuffer(0, _vertexBuffer);
            PastelGame.CommandList.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
            PastelGame.CommandList.SetPipeline(Pipeline);
            PastelGame.CommandList.DrawIndexed(
                4,
                1,
                0,
                0,
                0);
        }

        public override void Dispose()
        {
            Pipeline.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
        }
    }

    internal struct VertexPositionColor
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