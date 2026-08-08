window.downloadFileFromStream = async (fileName, contentType, contentArray) =>
{
    const blob = new Blob([new Uint8Array(contentArray)], { type: contentType });
    const url = URL.createObjectURL(blob);

    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName || 'download';

    document.body.appendChild(anchorElement);
    anchorElement.click();
    document.body.removeChild(anchorElement);

    URL.revokeObjectURL(url);
}
