const plugin = require('tailwindcss/plugin');

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      keyframes: {
        fromTop: {
          '0%': {
            position: 'relative',
            top: '-50px',
          },
          '100%': {
            top: '0',
          },
        },
        fadeIn: {
          '0%': {
            opacity: 0,
          },
          '100%': {
            opacity: 1,
          },
        },
      },
      animation: {
        fromTop: 'fromTop 0.5s ease-in-out',
        fadeIn: 'fadeIn 0.5s ease-in-out',
      },
    },
  },
  plugins: [
    plugin(({ addUtilities }) => {
      addUtilities({
        '.border-r-dashed': {
          borderRightStyle: 'dashed',
        },
        '.border-b-solid': {
          borderBottomStyle: 'solid',
        },
      });
    }),
  ],
};
