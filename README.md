# Summary 
This is an assignment for academic purpose at **Games and AI** course. Exercise on physics, steering behaviors and bird flocking without using any implemented Physics from unity.

**Note**: Library folders are missing due to size limitation

# Bird Flocking Implementation
![bird_flocking1](https://github.com/user-attachments/assets/f8fd6ac4-7488-40cf-b966-e994ec9fec6c)

Steering Behaviors that are implemented:
* **Cohesion**
* **Separation**
* **Alignment**

# Particle System Simulation
Particles are continuously created from a certain position in space, with random initial velocities, moving under the efect of gravity and a wind force. Particles are bouncing  with a coefficient of restitution on the walls of a transparent (or not) axis aligned cube that surrounds them. Position and velocity of each particle is calculated by simple **Euler** integration. Potential collisions with the cube and the particles are checked.
![particle_system](https://github.com/user-attachments/assets/09f8f83a-0f34-4b7c-9b3c-b69f8e3b3690)


# Steering Behaviors Implementation in 2D
* **Seek**
* **Pursue**
* **Arrive**
