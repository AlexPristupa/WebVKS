import { IBalancingAlgorithmConfig } from '@/domain/BalancingAlgorithm/IBalancingAlgorithmConfig.interface'
import { BalancingAlgorithm } from '@/domain/BalancingAlgorithm/BalancingAlgorithm.entity'

export class BalancingAlgorithmFactory {
  public static create(
    config: Array<IBalancingAlgorithmConfig>,
  ): Array<BalancingAlgorithm> {
    return config.map(item => new BalancingAlgorithm(item))
  }
}
