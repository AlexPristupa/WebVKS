import { BalancingAlgorithmName } from '@/domain/BalancingAlgorithm/BalancingAlgorithmName.enum'
import { IBalancingAlgorithmConfig } from '@/domain/BalancingAlgorithm/IBalancingAlgorithmConfig.interface'

export const BALANCING_ALGORITHM_CONFIG: Array<IBalancingAlgorithmConfig> = [
  {
    id: 1,
    privateName: BalancingAlgorithmName.roundRobin,
  },
  {
    id: 2,
    privateName: BalancingAlgorithmName.leastConnections,
  },
  {
    id: 3,
    privateName: BalancingAlgorithmName.routeMap,
  },
]
