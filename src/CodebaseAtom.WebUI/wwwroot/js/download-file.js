window.downloadFileFromStream = async (fileName, contentType, contentArray) =>
{
    // 1. Buat Blob dari array byte yang dikirim dari C#
    const blob = new Blob([new Uint8Array(contentArray)], { type: contentType });

    // 2. Buat URL objek sementara
    const url = URL.createObjectURL(blob);

    // 3. Buat elemen <a> tersembunyi untuk memicu download
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName || 'download';

    // 4. Klik otomatis & bersihkan elemen dari DOM
    document.body.appendChild(anchorElement);
    anchorElement.click();
    document.body.removeChild(anchorElement);

    // 5. Bebaskan memori URL
    URL.revokeObjectURL(url);
}
