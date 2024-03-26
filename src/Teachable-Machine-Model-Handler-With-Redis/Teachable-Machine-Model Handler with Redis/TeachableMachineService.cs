
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using Tensorflow;
using TensorFlow;

namespace Teachable_Machine_Model_Handler_with_Redis
{
    public class TeachableMachineService
    {
        private readonly TFGraph _modelGraph;
        public TeachableMachineService(string modelUrl, string metadataUrl)
        {
            _modelGraph = LoadModel(modelUrl, metadataUrl);
        }

        private TFGraph LoadModel(string modelUrl, string metadataUrl)
        {
            // Download model and metadata
            using (var client = new WebClient())
            {
                var modelBytes = client.DownloadData(modelUrl);
                var metadataBytes = client.DownloadData(metadataUrl);

                // Load model and metadata into TensorFlow Graph
                var graph = new TFGraph();
                graph.Import(new TFBuffer(modelBytes));

                // Tải metadata từ URL và parse nó thành đối tượng JSON
                string metadataJson = Encoding.UTF8.GetString(metadataBytes);
                JObject metadata = JObject.Parse(metadataJson);


                //var inputTensor = _modelGraph.Placeholder(TFDataType.Float, new TensorShape(-1, metadata.Size[0], metadata.Size[1], 3), "input");

                //// Lấy output node
                //var outputTensor = _modelGraph["output"][0];
                // Thực hiện các thao tác khác tùy thuộc vào cấu trúc của metadata và mô hình Teachable Machine

                return graph;
                // Optionally, you can perform additional processing here
            }
        }

        public float[] Predict(byte[] imageData)
        {
            // Chạy dự đoán bằng cách sử dụng mô hình đã tải và dữ liệu hình ảnh
            using (var session = new TFSession(_modelGraph))
            {
                // Lấy đầu vào và đầu ra từ đồ thị
                var inputTensor = _modelGraph["input"][0];
                var outputTensor = _modelGraph["output"][0];

                // Chuyển đổi dữ liệu hình ảnh thành tensor
                var imageTensor = new TFTensor(imageData);

                // Chạy session để thực hiện dự đoán
                var results = session.Run(new[] { inputTensor }, new[] { imageTensor }, new[] { outputTensor });

                // Trích xuất kết quả dự đoán và trả về
                return results[0].GetValue() as float[];
            }

        }
    }
}
