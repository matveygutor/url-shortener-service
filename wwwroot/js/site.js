// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const writeBtn = document.querySelector('.write-btn');
const inputEl = document.querySelector('.to-copy');

writeBtn.addEventListener('click', () => {
    const inputValue = inputEl.value.trim();
    if (inputValue) {
        navigator.clipboard.writeText(inputValue)
            .then(() => {
                if (writeBtn.innerText !== 'Copied') {
                    const originalText = writeBtn.innerText;
                    writeBtn.innerText = 'Copied';
                    setTimeout(() => {
                        writeBtn.innerText = originalText;
                    }, 6000);
                }
            })
            .catch(err => {
                console.log('Something went wrong', err);
            })
    }
});