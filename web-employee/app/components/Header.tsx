'use client';
import { signOut } from 'next-auth/react';
import Link from 'next/link';
import { MdCreditCard } from 'react-icons/md';
import { PiUserRectangleBold } from 'react-icons/pi';
import ThemeSwitch from './ThemeSwitch';
import { FiUsers } from 'react-icons/fi';

export const HeaderComp = () => {
    // const [token, _] = useLocalStorage('token', '');
    // const { data: session } = useSession();
    let session = {};

    const links = session
        ? [
              {
                  name: 'ПРОФИЛЬ',
                  icon: <PiUserRectangleBold size={60} />,
                  to: '/',
              },
              {
                  name: 'КЛИЕНТЫ',
                  icon: <FiUsers size={60} />,
                  to: '/users',
              },
              {
                  name: 'КРЕДИТЫ',
                  icon: <MdCreditCard size={60} />,
                  to: '/credits',
              },
          ]
        : [];

    const Button = ({ children, href, className }: any) => {
        return (
            <Link
                href={href}
                className={`${className} flex items-center gap-2 border-2 rounded-full px-4 lg:px-16 bg-bgColor2 dark:bg-bgColor3Dark text-3xl`}
            >
                {children}
            </Link>
        );
    };

    // useEffect(() => {
    //     if (token) {
    //         axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    //     }
    // }, []);

    return (
        <header className="flex flex-col lg:flex-row justify-between px-2 sm:px-12 py-4 w-full">
            <div className="flex flex-col lg:flex-row gap-4 lg:gap-12">
                {links.map(item => (
                    <Button key={item.name} href={item.to} className={''}>
                        {item.icon}
                        {item.name}
                    </Button>
                ))}
            </div>
            {session && (
                <>
                    <button
                        onClick={() => signOut()}
                        className="border-2 rounded-full px-4 lg:px-16 bg-bgColor2 dark:bg-bgColor3Dark text-3xl"
                    >
                        Выйти
                    </button>
                    <div className="absolute right-10 bottom-10">
                        <ThemeSwitch />
                    </div>
                </>
            )}
        </header>
    );
};
