import {NavBarButtons} from "./NavBarButtons.tsx";
import "./navbar.scss";

export const NavBar = () => {
    //const { scrollYProgress } = useScroll();
    //const scale = useTransform(scrollYProgress, [0, 0.3], [0.9, 0.40]);
    //const location = useTransform(scrollYProgress, [0, 0.31, 1], [0, 100, -1000])

    return (
        <>
            <div className="nav">
                <img src={""} alt={"logo"}/>
                <NavBarButtons></NavBarButtons>
                <img src={""} alt={"logo"}/>
            </div>
        </>
    );
};