import { useState, useEffect } from 'react';

export function useLocalStorage(key: string, fallbackValue: string) {
    const [value, setValue] = useState<string>(fallbackValue);
    useEffect(() => {
        const stored = localStorage.getItem(key);
        setValue(stored ? stored : fallbackValue);
    }, [fallbackValue, key]);

    useEffect(() => {
        if (value) localStorage.setItem(key, value);
    }, [key, value]);

    return [value, setValue] as const;
}
