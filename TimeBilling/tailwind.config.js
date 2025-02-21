/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Pages/**/*.{html,cshtml}",
        "./wwwroot/**/*.{html,htm}",
        "../timebilling.app/src/**.{html,js,vue}"
    ],
  theme: {
    extend: {},
  },
  plugins: [],
}

