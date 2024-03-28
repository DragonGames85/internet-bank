import type { Config } from 'tailwindcss';

const config: Config = {
    content: [
        './pages/**/*.{js,ts,jsx,tsx,mdx}',
        './components/**/*.{js,ts,jsx,tsx,mdx}',
        './app/**/*.{js,ts,jsx,tsx,mdx}',
    ],
    darkMode: 'class',
    theme: {
        extend: {
            backgroundImage: {
                'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
                'gradient-conic': 'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
            },
            colors: {
                textColor: 'var(--text-color)',
                bgColor: 'var(--bg-color)',
                bgColor2: 'var(--bg2-color)',
                bgColor3: 'var(--bg3-color)',

                textColorDark: 'var(--text-color-dark)',
                bgColorDark: 'var(--bg-color-dark)',
                bgColor2Dark: 'var(--bg2-color-dark)',
                bgColor3Dark: 'var(--bg3-color-dark)',

                main: '#3B82F6',
                mainHover: '#7BD1D2',
                whiteHover: '#D1E9EA',
                secondary: '#8799BD',
                transparentBlue: '#CBE2F7',
                accentBlue: '#287CC9',
                orange: '#B0654D',
                regularGray: '#D3D3D3',
                inputGray: '#B1B1B1',
                error: '#FF5B5B',
                bgDanger: 'rgba(255, 91, 91, .15)',
                danger: 'rgb(255, 91, 91)',
                dangerHover: 'rgba(255, 91, 91, 0.30)',
                inputFill: '#FFFFFF',
                divider: '#dedede',
                mainText: '#151837',
                secondaryText: '#9DA4AD',
                accentGrey: '#99A2AD',
                mainLight: '#EBF2F2',
                pageBackground: '#f5f5f5',
                dimText: 'rgba(0, 0, 0, .5)',
                footerBg: '#1C1F39',
                greyHover: '#F0F3F3',
                lightRed: '#FFEAEB',
                gold: '#D4B984',
                whiteBorder: '#E2E8F0',
                sky: '#93C5FD',
                darkBlue: '#18338C',
            },
            borderWidth: {
                1: '1px',
            },
            textShadow: {
                outline: '-1px -1px 0 #000, 1px -1px 0 #000, -1px 1px 0 #000, 1px 1px 0 #000',
            },
        },
    },
    plugins: [],
};
export default config;
