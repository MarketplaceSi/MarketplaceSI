using Dto.Responses;

namespace Kernel.Products;
public class UploadFilesPayload : Payload
{
    public UploadFilesPayload(List<S3UploadFiles> uploadUrls)
    {
        UploadUrls = uploadUrls;
    }
    public UploadFilesPayload(IReadOnlyList<Error> errors)
        : base(errors)
    {
    }
    public List<S3UploadFiles>? UploadUrls { get; set; }
}
