'use client';

import { useTheme } from 'next-themes';
import { BiSolidMoon } from 'react-icons/bi';
import { PiSunDimFill } from 'react-icons/pi';
import { api } from '../api';

const ThemeSwitch: React.FC<{ className?: string }> = ({ className }) => {
    const { theme, setTheme } = useTheme();

    const toggleTheme = async () => {
        try {
            api.auth.setTheme(theme !== 'light');
            setTheme(theme === 'light' ? 'dark' : 'light');
        } catch (error) {}
    };

    // useEffect(() => {
    //     try {
    //         const userLocal = JSON.parse(localStorage.getItem('user') ?? '{}');
    //         setTheme(userLocal.isLightTheme == 'True' ? 'light' : 'dark');
    //     } catch (err) {
    //         console.error(err);
    //     }
    // }, []);

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
