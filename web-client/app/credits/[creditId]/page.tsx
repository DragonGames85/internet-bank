'use client';

import { LOREM } from '@/app/config';
import { useEffect } from 'react';
import { MdOutlineDisabledByDefault } from 'react-icons/md';

const Credits = ({ params }: { params: { creditId: string } }) => {
    useEffect(() => {
        document.documentElement.style.setProperty('--background-end-rgb', '120 0 120');
    }, []);
    
    return (
        <div className={`flex flex-col items-start border-[1px] rounded-3xl p-6 bg-fuchsia-950 h-[290px] gap-1`}>
            <div className="flex w-full text-2xl justify-between">
                <p className={`self-center underline underline-offset-8 text-white`}>{`Кредит ${params.creditId}`}</p>
                <div className="flex gap-2 items-center">
                    <MdOutlineDisabledByDefault color="red" size={34} />
                </div>
            </div>
            <div className=" h-full py-4 w-full rounded-3xl self-center text-xl overflow-hidden text-ellipsis">
                <p className={`text-blue-400 w-full`}>{LOREM}</p>
            </div>
        </div>
    );
};

export default Credits;
