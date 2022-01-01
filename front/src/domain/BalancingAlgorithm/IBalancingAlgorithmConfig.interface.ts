import { BalancingAlgorithmName } from '@/domain/BalancingAlgorithm/BalancingAlgorithmName.enum'

export interface IBalancingAlgorithmConfig {
  id: number
  privateName: BalancingAlgorithmName
}
