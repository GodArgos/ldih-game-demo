# Like Dios, Ignora Humanos

## 🇪🇸 Español

### 🎮 Descripción
Prototipo 2D top-down desarrollado por comisión para presentar la idea base de un videojuego. Incluye el inicio del juego y mecánicas básicas de movimiento e interacción.

El foco principal del proyecto es una mecánica de visibilidad limitada similar a la utilizada en *Among Us*.

### 🛠️ Tecnologías
- Unity
- Desarrollo 2D

### ⚙️ Características Técnicas
- Sistema de iluminación dinámica y campo de visión
- Oclusión visual mediante geometría y raycasting
- Uso de shaders y stencil buffer

### 🔬 Detalle Técnico Destacado
- Se disparan raycasts desde la posición del jugador hacia múltiples direcciones
- A partir de los puntos de colisión se genera un **mesh dinámico (triangle fan)**
- Este mesh representa el área visible del jugador
- Un **shader con stencil buffer** recorta un plano oscuro usando la forma del mesh

Esto evita ver a través de paredes o dentro de habitaciones cerradas.

### 📌 Estado
✔ Prototipo completado

![LDIH_1](/Md_Resources/LDIH_1.PNG)
![LDIH_2](/Md_Resources/LDIH_3.PNG)
---

## 🇬🇧 English

### 🎮 Description
2D top-down prototype developed under commission to present the core idea of a videogame. It includes the game’s introduction and basic movement and interaction mechanics.

The main focus is a limited-visibility system similar to the one used in *Among Us*.

### 🛠️ Technologies
- Unity
- 2D Game Development

### ⚙️ Technical Features
- Dynamic lighting and field-of-view system
- Visual occlusion using geometry and raycasting
- Shader-based rendering using stencil buffers

### 🔬 Technical Highlight
- Raycasts are emitted from the player position in multiple directions
- Collision points are used to generate a **dynamic mesh (triangle fan)**
- The mesh represents the player’s current visible area
- A **stencil buffer shader** cuts the visible shape from a dark overlay

This prevents the player from seeing through walls or into closed rooms.

### 📌 Status
✔ Completed prototype

![LDIH_3](/Md_Resources/LDIH_5.PNG)
![LDIH_4](/Md_Resources/LDIH_6.PNG)