module.exports = {
    mode: 'jit',
    content: ['./**/*.razor', './wwwroot/index.html'],
    theme: {
        extend: {
            colors: {
                slack_bg: "#3E103F",
                slack_nav: "#2B092A",
                active: "#0065b3"
            }
        },
    },
    plugins: [],
};