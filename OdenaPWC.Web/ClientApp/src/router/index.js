import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home.vue";
import Historico from "@/components/Historico.vue";
import Alertas from "@/components/Alertas.vue";
import Forecast from "@/components/Forecast.vue";

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/Historico",
        name: "Historico",
        component: Historico,
    },
    {
        path: "/Alertas",
        name: "Alertas",
        component: Alertas,
    },
    {
        path: "/Forecast",
        name: "Forecast",
        component: Forecast,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;