using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class CollidableObject : asd.TextureObject2D
    {
        //半径
        public float Radius = 0.0f;

        //引数に指定したコリダブルオブジェクトと自分が衝突しているか、を返す。
        public bool IsCollide(CollidableObject o)
        {
            //二点間の距離がお互いの半径の和より小さい場合にはtrueを返す。
            return (o.Position - Position).Length < Radius + o.Radius;
        }

        //引数に指定したコリダブルオブジェクトと自分が衝突しそうか、を返す。
        public bool IsCollide2(CollidableObject o)
        {
            //二点間の距離がお互いの半径の和-1より小さい場合にはtrueを返す。
            return (o.Position - Position).Length < Radius + o.Radius - 1.0f;
        }

        //衝突時の処理を行うメソッドを実装する。
        public virtual void OnCollide(CollidableObject obj)
        {

        }
    }
}
