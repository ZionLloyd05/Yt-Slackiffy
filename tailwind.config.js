module.exports = {
    mode: 'jit',
    content: ['./**/*.razor', './wwwroot/index.html'],
    theme: {
        extend: {
            colors: {
                slack_bg: "",
                slack_nav: "#3F0F40"
            }
        },
    },
    plugins: [],
};