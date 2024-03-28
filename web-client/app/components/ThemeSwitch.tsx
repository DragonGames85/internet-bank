'use client';

import { PiSunDimFill } from 'react-icons/pi';
import { BiSolidMoon } from 'react-icons/bi';
import { useTheme } from 'next-themes';
import { api } from '../api';

const ThemeSwitch: React.FC<{ className?: string }> = ({ className }) => {
    const { theme, setTheme } = useTheme();

    const toggleTheme = async () => {
        try {
            // api.auth.options('1', { theme: theme === 'light' ? 'dark' : 'light', hidden_accounts: [] });
            setTheme(theme === 'light' ? 'dark' : 'light');
        } catch (error) {}
    };

    const isActive = theme === 'light';

    const switchClasses = `flex items-center justify-center w-6 h-6 text-dark bg-white rounded-full transform ${
        isActive ? 'translate-x-0' : 'translate-x-6'
    } transition-transform duration-500 ease-in-out`;

    return (
        <div
            className={`relative w-14 h-8 rounded-full p-1 cursor-pointer bg-[#ccc] ${className}`}
            onClick={toggleTheme}
        >
            <button className={switchClasses}>
                {isActive ? <PiSunDimFill size={16} /> : <BiSolidMoon color="black" />}
            </button>
        </div>
    );
};

export default ThemeSwitch;
