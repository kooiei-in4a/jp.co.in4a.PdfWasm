using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace jp.co.in4a.PdfWasm.PdfUtils
{

    public static class PdfPasswordChecker
    {
        /// <summary>
        /// PDFのパスワード保護状態をチェック
        /// </summary>
        /// <param name="pdfBytes">PDFのバイト配列</param>
        /// <returns>
        /// (HasUserPassword: ユーザーパスワード有無,
        ///  HasOwnerPassword: オーナーパスワード有無)
        /// </returns>
        public static (bool HasUserPassword, bool HasOwnerPassword)
            CheckPasswordProtection(byte[] pdfBytes,string password)
        {
            bool hasUserPassword = false;
            bool hasOwnerPassword = false;

            try
            {
                // UserPasswordチェック（パスワードなしで開けるか確認）
                using (var stream = new MemoryStream(pdfBytes))
                using (var doc = new PdfLoadedDocument(stream,password))
                {
                   
                    if (!doc.IsEncrypted)
                    {
                        // PDFが暗号化されていない場合、パスワードは存在しない
                        hasUserPassword = false; // ユーザーパスワードは存在する
                        hasOwnerPassword = false; // オーナーパスワードも存在する可能性あり
                    }
                    else
                    {
                        // TODO:詳細チェックは後で実装
                        hasUserPassword = true;
                        hasOwnerPassword = true;
                    }
                }
            }
            catch (PdfInvalidPasswordException ex) 
            {
                hasUserPassword = true;
                hasOwnerPassword = true; 
            }

            return (hasUserPassword, hasOwnerPassword);
        }
    }

}
