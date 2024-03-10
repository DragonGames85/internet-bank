import axios from 'axios';

export type divProps = React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement>;
export type ModalProps = { isOpen: boolean; onClose: () => void };
export const LOREM =
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.';

export type IdName = { id: string | number; name: string };

export const adaptiveClass = (
    standard: string,
    values: [string | number, string | number, string | number, string | number, string | number, string | number]
) => {
    let className = '',
        adaptiveArray = ['', 'sm:', 'md:', 'lg:', 'xl:', '2xl:'];

    values.forEach((value, index) => {
        className += `${adaptiveArray[index]}${standard}-${value} `;
    });
    return className;
};

export const animationVariants = {
    container: {
        hidden: { opacity: 1, scale: 0 },
        visible: {
            opacity: 1,
            scale: 1,
            transition: {
                delayChildren: 0.1,
                staggerChildren: 0.1,
            },
        },
    },
    item: {
        hidden: { y: 20, opacity: 0 },
        visible: {
            y: 0,
            opacity: 1,
        },
    },
};

export const setURL = (url: number) => {
    axios.defaults.baseURL = `https://localhost:${url}/api`;
};
