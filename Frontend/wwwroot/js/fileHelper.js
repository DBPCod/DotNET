window.readFileAsBase64 = (inputId) => {
    return new Promise((resolve, reject) => {
        const input = document.getElementById(inputId);
        if (!input || !input.files || input.files.length === 0) {
            resolve(null);
            return;
        }

        const file = input.files[0];
        const reader = new FileReader();

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = () => {
            reject(reader.error);
        };

        reader.readAsDataURL(file);
    });
};

window.getFileFromInput = (inputId) => {
    const input = document.getElementById(inputId);
    if (!input || !input.files || input.files.length === 0) {
        return null;
    }
    return input.files[0];
};
