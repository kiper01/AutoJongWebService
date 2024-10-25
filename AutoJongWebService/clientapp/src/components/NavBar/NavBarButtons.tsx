import {NavBarButton} from "./NavBarButton.tsx";

export const NavBarButtons = () => {
    return (
    <div className="navbar-buttons">
        <NavBarButton 
            title={"Главная"}
        />
        <NavBarButton 
            title={"Автоподбор"}
        />
        <NavBarButton 
            title={"О нас"}
        />
        <NavBarButton
            title={"Контакты"}
        />
    </div>)
}