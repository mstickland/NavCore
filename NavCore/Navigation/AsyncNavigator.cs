using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NavCore.Navigation {
    
    /// <summary>
    /// Very Experimental, please ignore
    /// I may just need to scrap this
    /// </summary>
    public class AsyncNavigator<TNavNode> : Navigator<TNavNode> where TNavNode : class, INavigationNode {

        public static int DelayTime { get; set; } = 500;
        public static int SmallDelayTime { get; set; } = 10;


        public NavigationResult<TNavNode> NavigateSlow2(TNavNode start, TNavNode destination, List<TNavNode> route, ref bool cancel, ref bool lck) {
            var result = new NavigationResult<TNavNode>();
            lck = false;
            route.Clear();
            cancel = false;

            result.Success = NavigationRecursiveSlow2(start, destination, new List<TNavNode>(), route, ref cancel, ref lck);
            result.Route = route;
            if (cancel)
                route.Clear();

            lck = false;

            return result;
        }

        private bool NavigationRecursiveSlow2(TNavNode location, TNavNode destination, List<TNavNode> vistedPlaces, List<TNavNode> route, ref bool cancel, ref bool lck) {

            if (cancel == true) {
                route.Clear();
                return false;
            }

            lck = true;
            route.Add(location);
            lck = false;
            Thread.Sleep(DelayTime);

            if (ReferenceEquals(location, destination))
                return true;

            vistedPlaces.Add(location);




            var unvisitedConnections = location.Connections.
                                            Except(vistedPlaces).
                                            OrderBy(loc => NodeMath.GetDistanceSq(loc.Position, location.Position)    * ImmediateDistWeight +
                                                           NodeMath.GetDistanceSq(loc.Position, destination.Position) * DestinationDistWeight
                                            );


            foreach (var connection in unvisitedConnections) {
                if (cancel == true) {
                    route.Clear();
                    return false;
                }

                if (NavigationRecursiveSlow2((TNavNode)connection, destination, vistedPlaces, route, ref cancel, ref lck))
                    return true;

            }

            lck = true;
            route.Remove(location);
            lck = false;
            Thread.Sleep(DelayTime);

            return false;

        }

        public async Task<NavigationResult<TNavNode>> NavigateSlowly(TNavNode start, TNavNode destination, List<TNavNode> route) {
            var result = new NavigationResult<TNavNode>();


            route.Clear();
            try {
                result.Success = await NavigationRecursiveSlowly(start, destination, new List<TNavNode>(), route);
                result.Route = route;
            } catch (OperationCanceledException) {
                Debug.WriteLine("!Cancel");
            }

            return result;
        }

        private async Task<bool> NavigationRecursiveSlowly(TNavNode location, TNavNode destination, List<TNavNode> vistedPlaces, List<TNavNode> route) {
            const int DelayTime = 700;
            route.Add(location);

            if (ReferenceEquals(location, destination))
                return true;

            vistedPlaces.Add(location);
            await Task.Delay(DelayTime);


            var unvisitedConnections = location.Connections.
                                            Except(vistedPlaces).
                                            OrderBy(loc => NodeMath.GetDistanceSq(loc.Position, location.Position)    * ImmediateDistWeight +
                                                           NodeMath.GetDistanceSq(loc.Position, destination.Position) * DestinationDistWeight
                                            );


            foreach (var connection in unvisitedConnections) {
                if (await NavigationRecursiveSlowly((TNavNode)connection, destination, vistedPlaces, route))
                    return true;
            }

            route.Remove(location);
            await Task.Delay(DelayTime);

            return false;

        }


    }
}
