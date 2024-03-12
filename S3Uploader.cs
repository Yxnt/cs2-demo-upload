

using Amazon.S3;
using Amazon.S3.Model;
using Serilog;
using System.Net;

namespace csgo2_demo_s3
{
    public class S3Uploader
    {
        private string _ak = string.Empty;
        private string _sk = string.Empty;
        private string _endpoint = string.Empty;
        private string _bucket = string.Empty;
        public S3Uploader(string ak,string sk,string endpoint,string bucket)
        {
            _ak = ak;
            _sk = sk;
            _endpoint = endpoint;
            _bucket = bucket;
        }

        private AmazonS3Client s3Client()
        {
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = _endpoint;
            AmazonS3Client s3Client = new AmazonS3Client(
                _ak,
                _sk,
                config
            );
            
            return s3Client;
        }

        public async Task<bool> UploadDemoAsync(string demoName,string demoPath)
        {
            var client = s3Client();
            PutObjectRequest request = new PutObjectRequest();
            request.BucketName = _bucket;
            request.Key = demoName;
            request.ContentType = "application/zip";
            request.FilePath = demoPath;
            
            PutObjectResponse response = await client.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
