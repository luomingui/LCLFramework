
namespace LCL.MetaModel
{
    /// <summary>
    /// 树型实体的编码生成规则
    /// </summary>
    public class TreeCodeOption
    {
        public static readonly TreeCodeOption Default = new TreeCodeOption
        {
            Seperator = '.',
            Layers = new string[][] {
                new string[]{"1", "2", "3", "4", "5", "6", "7", "8", "9"}
            }
        };

        /// <summary>
        /// 每一层间的分隔符
        /// </summary>
        public char Seperator;

        /// <summary>
        /// 每一层的字符串定义
        /// </summary>
        public string[][] Layers;

        private string[] GetLayer(int index)
        {
            if (index < this.Layers.Length)
            {
                return this.Layers[index];
            }
            return this.Layers[this.Layers.Length - 1];
        }

        /// <summary>
        /// 通过父对象的编码以及当前的索引来生成可用的树型编码
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="nodeIndex"></param>
        /// <returns></returns>
        public string CalculateCode(string parentCode, int nodeIndex)
        {
            int layerIndex = -1;
            if (!string.IsNullOrEmpty(parentCode))
            {
                //父结点中有几个分隔符，就表示第几层。
                for (int i = 0; i < parentCode.Length; i++) { if (parentCode[i] == Seperator) layerIndex++; }
                if (layerIndex == -1) layerIndex = 0;
            }
            else
            {
                layerIndex = 0;
                parentCode = string.Empty;
            }
            var code = this.GetSingleCode(layerIndex, nodeIndex);
            return parentCode + code + Seperator;
        }

        private string GetSingleCode(int layerIndex, int nodeIndex)
        {
            var layer = this.GetLayer(layerIndex);
            if (nodeIndex < layer.Length)
            {
                return layer[nodeIndex];
            }
            return layer[layer.Length - 1];
        }
    }
}
