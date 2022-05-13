module.exports = {
    mode: 'jit',
    content: ['./**/*.razor', './wwwroot/index.html'],
    theme: {
        extend: {
            colors: {
                slack_bg: "#3F0F40"
            }
        },
    },
    plugins: [],
};