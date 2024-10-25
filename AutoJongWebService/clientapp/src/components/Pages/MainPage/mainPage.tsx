import React from "react";
import "./mainpage.scss";
import {WelcomeSection} from "./Sections/WelcomeSection.tsx";
import {WorkSchemeSection} from "./Sections/WorkSchemeSection.tsx";

export const MainPage: React.FC = () => {
    return <>
        <WelcomeSection/>
        <WorkSchemeSection/>
    </>

}