import { useEffect } from 'react';

export const useColorEffect = (color: string) => {
    useEffect(() => {
        document.documentElement.style.setProperty('--background-end-rgb', color);
    }, []);
};
