using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace Lab3
{
    internal class View
    {
        int vbo_position;
        void LoadShader(String filename, ShaderType type, int program, out int adress) {
            adress = GL.CreateShader(type);
            using(System.IO.StreamReader sr = new System.IO.StreamReader(filename)) 
            {
                GL.ShaderSource(adress, sr.ReadToEnd());
            }
            GL.CompileShader(adress);
            GL.AttachShader(program, adress);
            Console.WriteLine(GL.GetShaderInfoLog(adress));
       }
        void InitShaders() { 
            int BasicProgramID = GL.CreateProgram();
            LoadShader("raytracing.vert", 
                ShaderType.VertexShader,
                BasicProgramID, 
                out BasicVertexShader);
            LoadShader("raytracing.frag",
                ShaderType.FragmentShader,
                BasicProgramID,
                out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID,
                GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
            Vector3[] vertdata = new Vector3[] {
            new Vector3(-1f,-1f,0f),
            new Vector3(1f,-1f,0f),            
            new Vector3(1f,-1f,0f),
            new Vector3(-1f,1f,0f) };

            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                (IntPtr)(vertdata.Length * Vector3.SizeInBytes), 
                vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float,
                false, 0, 0);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect, aspect);
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        
        }
    }
}
