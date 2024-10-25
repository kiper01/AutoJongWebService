import { useGlitch } from 'react-powerglitch'

interface IProps {
    //Текст кнопки
    title : string
}

export const NavBarButton : React.FC<IProps> = ({title}) => {
    const glitch = useGlitch({
        playMode: 'hover',
            createContainers: true,
            hideOverflow: false,
            timing: {
                duration: 150,
                iterations: 1
            },
        shake: {
            velocity: 15,
                amplitudeX: 0.2,
                amplitudeY: 0.2
        },
        slice: {
            count: 10,
            velocity: 20,
            minHeight: 0.02,
            maxHeight: 0.15,
            hueRotate: true
        }
     });

    return <a className="navbar-button" ref={glitch.ref}>
        {title}
    </a>
}
    

