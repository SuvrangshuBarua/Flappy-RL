# Overview
A base implementation of the classic Flappy Bird game, trained in Unity using the `ml-agents` library.  
The training is done using the **Proximal Policy Optimization (PPO)** technique as a reinforcement learning method.

## 🧠 How the AI Is Trained

The Flappy Bird agent is trained using **reinforcement learning** with the **Unity ML-Agents Toolkit**, specifically using the **Proximal Policy Optimization (PPO)** algorithm.

---

### 🎯 Agent Design

- The `Bird` class inherits from `Agent`, enabling Unity ML-Agents integration.
- **Observations** (fed into the neural network):
  - Bird’s normalized Y-position
  - Bird’s vertical velocity
  - Distance and height of the next pipe
- **Actions**:
  - `0 = do nothing`
  - `1 = flap (adds upward force)`

---

### 💡 Reward Strategy

| Event                  | Reward           |
|------------------------|------------------|
| Survives one frame     | +Time.deltaTime  |
| Hits pipe or ground    | -1               |
| Episode ends (death)   | EndEpisode()     |

This reward shaping encourages survival and penalizes crashing.

---

### ⚙️ Training Configuration (`training_config.yaml`)

```yaml
behaviors:
    flAppy-bIrd:
        trainer_type: ppo    
        summary_freq: 10000
        network_settings:
            hidden_units: 256
