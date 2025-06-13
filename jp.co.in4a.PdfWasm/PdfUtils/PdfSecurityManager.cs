using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.Drawing;
using System.IO;

namespace jp.co.in4a.PdfWasm.PdfUtils
{
    public class PdfSecurityManager
    {
        /// <summary>
        /// PDFのバイト配列にパスワードと操作権限を設定し、結果を新しいバイト配列として返します。
        /// </summary>
        /// <param name="inputPdfBytes">入力となるPDFのバイト配列</param>
        /// <param name="userPassword">閲覧者用のユーザーパスワード</param>
        /// <param name="ownerPassword">管理者用のオーナーパスワード</param>
        /// <param name="permissions">ユーザーパスワードで開いた際に許可する操作</param>
        /// <returns>セキュリティが適用された新しいPDFのバイト配列</returns>
        public static byte[] ApplySecurity(byte[] inputPdfBytes, string userPassword, string ownerPassword, PdfPermissionsFlags permissions)
        {
            if (inputPdfBytes == null || inputPdfBytes.Length == 0)
            {
                throw new ArgumentException("入力されたPDFのバイト配列がnullまたは空です。", nameof(inputPdfBytes));
            }

            // 1. バイト配列からPdfLoadedDocumentオブジェクトを作成
            using (PdfLoadedDocument document = new PdfLoadedDocument(inputPdfBytes))
            {
                // 2. セキュリティオブジェクトを取得
                PdfSecurity security = document.Security;

                // 3. 暗号化アルゴリズムとキーサイズを設定
                security.Algorithm = PdfEncryptionAlgorithm.AES;
                security.KeySize = PdfEncryptionKeySize.Key256Bit;

                // 4. ユーザーパスワードとオーナーパスワードを設定
                security.UserPassword = userPassword;
                security.OwnerPassword = ownerPassword;

                // 5. 許可する権限を設定
                security.Permissions = permissions;

                // 6. ドキュメントをMemoryStreamに保存
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream);

                    // 7. MemoryStreamの内容をバイト配列として返す
                    return stream.ToArray();
                }
            }
        }
    }
}
