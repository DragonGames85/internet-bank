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
    console.log(className)
    return className;
};
