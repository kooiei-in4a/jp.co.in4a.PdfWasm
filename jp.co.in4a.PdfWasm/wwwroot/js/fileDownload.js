// wwwroot/js/fileDownload.js

/**
 * Base64エンコードされたファイルデータをダウンロードする
 * @param {string} base64Data - Base64エンコードされたファイルデータ
 * @param {string} fileName - ダウンロードするファイル名
 */
window.downloadFile = function (base64Data, fileName) {
    try {
        // Base64データをバイナリデータに変換
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);

        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        // Uint8Arrayを作成
        const byteArray = new Uint8Array(byteNumbers);

        // Blobオブジェクトを作成（PDFファイル用のMIMEタイプを指定）
        const blob = new Blob([byteArray], { type: 'application/pdf' });

        // ダウンロード用のURLを作成
        const url = window.URL.createObjectURL(blob);

        // 一時的なaタグを作成してダウンロードを実行
        const downloadLink = document.createElement('a');
        downloadLink.href = url;
        downloadLink.download = fileName;
        downloadLink.style.display = 'none';

        // DOMに追加してクリック、その後削除
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);

        // メモリリークを防ぐためURLを解放
        window.URL.revokeObjectURL(url);

    } catch (error) {
        console.error('ファイルダウンロード中にエラーが発生しました:', error);
        alert('ファイルのダウンロードに失敗しました。');
    }
};