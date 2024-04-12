import axios from 'axios';
import 'dotenv/config';

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

const productionMode = process.env.NEXT_PUBLIC_PRODUCTION_MODE === 'true';
export const authAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_AUTH_APP_URL_PROD : process.env.NEXT_PUBLIC_AUTH_APP_URL_LOCAL) ?? '';
export const coreAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_CORE_APP_URL_PROD : process.env.NEXT_PUBLIC_CORE_APP_URL_LOCAL) ?? '';
export const creditAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_CREDIT_APP_URL_PROD : process.env.NEXT_PUBLIC_CREDIT_APP_URL_LOCAL) ?? '';
export const webauthAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_WEBAUTH_APP_URL_PROD : process.env.NEXT_PUBLIC_WEBAUTH_APP_URL_LOCAL) ?? '';
export const clientAppUrl =
    (productionMode ? process.env.NEXT_PUBLIC_CLIENT_APP_URL_PROD : process.env.NEXT_PUBLIC_CLIENT_APP_URL_LOCAL) ?? '';

export const setURL = (url: string) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('token') ?? ''}`;
    axios.defaults.baseURL = `${url}/api`;
};
