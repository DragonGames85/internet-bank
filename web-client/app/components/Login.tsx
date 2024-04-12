import Link from 'next/link';
import { clientAppUrl, webauthAppUrl } from '../config';

const Login = () => {
    return (
        <div className="flex-center flex-col">
            <h1>ВЫ НЕ ВОШЛИ В СИСТЕМУ</h1>
            <Link href={`${webauthAppUrl}?home=${clientAppUrl.replace(/(^\w+:|^)\/\//, '')}`} passHref>
                <button className="border-2 rounded-full bg-bgColor2 dark:bg-bgColor3Dark text-3xl text-center self-center p-4 px-16">
                    АВТОРИЗОВАТЬСЯ
                </button>
            </Link>
        </div>
    );
};

export default Login;
